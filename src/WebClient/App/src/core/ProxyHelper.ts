import { interfaces } from "inversify";
import { ProxyInvoker } from "./ProxyInvoker";
import { HeaderProvider } from "./HeaderProvider";
import { RoundRobinManager } from "./RoundRobinManager";
import { TYPES } from "../actions";
import { ApiContract } from "../api/ApiContract";

export namespace ProxyHelper {
  export function createProxy<T extends ApiContract>(
    // tslint:disable-next-line:no-any
    type: any,
    resolver: interfaces.Container,
    serviceIdentifier: string | symbol
  ): T {
    const headerProvider = resolver.get<HeaderProvider>(TYPES.HeaderProvider);
    const roundRobinManager = resolver.get<RoundRobinManager>(
      TYPES.RoundRobinManager
    );
    const instance = new type();

    Object.getOwnPropertyNames(instance).forEach(key => {
      let prop = Object.getOwnPropertyDescriptor(instance, key);
      if (prop !== undefined) {
        if (key !== "getType" && typeof prop.value === "function") {
          const invoker = new ProxyInvoker(key, roundRobinManager, headerProvider);
          instance[key] = new Proxy(instance[key], invoker);
        }
      }
    });
    
    return instance;
  }
}
