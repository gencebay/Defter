import { interfaces } from "inversify";
import { ProxyHelper } from "../Core/ProxyHelper";
import { ApiContract } from "../Api/";

export namespace InversifyHelper {
  export function RegisterApi<T extends ApiContract>(
    // tslint:disable-next-line:no-any
    type: any,
    resolver: interfaces.Container,
    serviceIdentifier: string | symbol
  ) {
    resolver
      .bind<interfaces.Factory<T>>(serviceIdentifier)
      .toFactory<T>((context: interfaces.Context) => {
        return () => {
          return ProxyHelper.createProxy<T>(type, resolver, serviceIdentifier);
        };
      });
  }
}
