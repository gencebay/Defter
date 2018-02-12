import resolver from "./inversify.config";
import { AuthorizeApi } from "./api";
import { APIs, TYPES } from "./actions";
import { ProxyHelper } from "./core/ProxyHelper";
import { AlbumApi } from "./api/AlbumApi";
import { RoundRobinManager } from "./core/RoundRobinManager";

export namespace Factory {
  export function createAuthorizeApi(): AuthorizeApi {
    return ProxyHelper.createProxy<AuthorizeApi>(
      AuthorizeApi,
      resolver,
      APIs.AuthorizeApi
    );
  }

  export function createAlbumApi(): AlbumApi {
    return ProxyHelper.createProxy<AlbumApi>(AlbumApi, resolver, APIs.AlbumApi);
  }

  export function getDefaultMainUrl(): string {
    const roundRobinManager = resolver.get<RoundRobinManager>(
      TYPES.RoundRobinManager
    );

    return roundRobinManager.getAssetsUri();
  }
}
