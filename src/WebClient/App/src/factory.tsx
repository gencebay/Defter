import resolver from "./inversify.config";
import { AuthorizeApi, AlbumApi } from "./Api";
import { TypeConstants } from "./Constants";
import { ApiConstants } from "./Constants";
import { ProxyHelper, RoundRobinManager } from "./Core";

export namespace Factory {
  export function createAuthorizeApi(): AuthorizeApi {
    return ProxyHelper.createProxy<AuthorizeApi>(
      AuthorizeApi,
      resolver,
      ApiConstants.AuthorizeApi
    );
  }

  export function createAlbumApi(): AlbumApi {
    return ProxyHelper.createProxy<AlbumApi>(
      AlbumApi,
      resolver,
      ApiConstants.AlbumApi
    );
  }

  export function getDefaultMainUrl(): string {
    const roundRobinManager = resolver.get<RoundRobinManager>(
      TypeConstants.RoundRobinManager
    );

    return roundRobinManager.getAssetsUri();
  }
}
