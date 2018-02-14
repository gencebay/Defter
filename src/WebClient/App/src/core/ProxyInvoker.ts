import { DefaultHeaderProvider, RoundRobinManager } from "../Core";
import { injectable } from "inversify";
import { getMethodDefinition } from "../Decorators";
import { ApiContract } from "../Api";
import { ObjectHelper } from "../Utils/ObjectHelper";
import { HttpMethodDefinition } from "../Models";

@injectable()
class ProxyInvoker {
  public key: string;
  public roundRobinManager: RoundRobinManager;
  public headerProvider: DefaultHeaderProvider;
  public constructor(
    key: string,
    roundRobinManager: RoundRobinManager,
    headerProvider: DefaultHeaderProvider
  ) {
    this.key = key;
    this.roundRobinManager = roundRobinManager;
    this.headerProvider = headerProvider;
  }

  // tslint:disable-next-line:no-any
  async apply(target: any, thisArg: any, argArray?: any): Promise<any> {
    let requestUrl = this.getUrl(thisArg);
    let methodDefinition = getMethodDefinition(thisArg, this.key);
    if (!methodDefinition) {
      methodDefinition = <HttpMethodDefinition>{};
      methodDefinition.httpMethod = "get";
      methodDefinition.body = "";
    }

    const httpMethod = methodDefinition.httpMethod;
    const body = methodDefinition.body;

    let values = argArray;
    if (argArray && argArray.length > 0) {
      if (argArray.length === 1) {
        values = argArray[0];
      }
    }

    const requestInit: RequestInit = {
      method: httpMethod
    };

    const headers = new Headers();
    this.headerProvider.headers.forEach((value, key) => {
      headers.append(key, value);
    });

    if (httpMethod === "post") {
      if (body === "json") {
        headers.append("Content-Type", "application/json; charset=utf-8");
        requestInit.body = JSON.stringify(values);
      } else {
        const formData: FormData = new FormData();
        // tslint:disable-next-line:no-any
        argArray.forEach((element: any) => {
          for (var key in element) {
            if (element[key]) {
              formData.append(key, element[key]);
            }
          }
        });
        requestInit.body = formData;
      }
    } else {
      const paramNames = ObjectHelper.getParamNames(target);
      const argsObj = {};
      // tslint:disable-next-line:no-any
      paramNames.forEach((value: any, index: number) => {
        argsObj[value] = values[index];
      });
      const queryString = ObjectHelper.toQueryString(argsObj);
      requestUrl = `${requestUrl}?${queryString}`;
    }

    requestInit.headers = headers;
    const response = await fetch(requestUrl, requestInit);

    if (response.status < 400) {
      var contentType = response.headers.get("content-type");
      if (contentType && contentType.includes("application/json")) {
        return response.json();
      }
    }

    throw new TypeError("Invalid API result.");
  }

  private getUrl = (instance: ApiContract) => {
    let url = this.roundRobinManager.roundRobinUri(instance.regionKey);
    let route = instance.routeTemplate;

    if (!url.endsWith("/")) {
      url = `${url}/`;
    }

    if (route.startsWith("/")) {
      route = route.substring(1, route.length);
    }

    let methodName = this.key;
    if (methodName.startsWith("/")) {
      methodName = methodName.substring(1, methodName.length);
    }

    const requestPath = `${url}${route}/${methodName}`;
    return requestPath;
    // tslint:disable-next-line:semicolon
  };
}

export { ProxyInvoker };
