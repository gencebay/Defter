import "reflect-metadata";

import { Container } from "inversify";

import { TYPES, APIs } from "./actions";
import {
  HeaderProvider,
  DefaultHeaderProvider,
  RestClient,
  DefaultRestClient,
  Settings,
  DefaultSettings,
  ProxyManager,
  RoundRobinManager,
  DefaultProxyManager
} from "./core";

import { AuthorizeApi } from "./api";
import { InversifyHelper } from "./utils";

let resolver = new Container();
resolver.bind<Settings>(TYPES.Settings).to(DefaultSettings).inSingletonScope();
resolver
  .bind<RoundRobinManager>(TYPES.RoundRobinManager)
  .to(RoundRobinManager)
  .inSingletonScope();
resolver
  .bind<HeaderProvider>(TYPES.HeaderProvider)
  .to(DefaultHeaderProvider)
  .inSingletonScope();
resolver.bind<RestClient>(TYPES.RestClient).to(DefaultRestClient);
resolver.bind<ProxyManager>(TYPES.ProxyManager).to(DefaultProxyManager);

// APIs
InversifyHelper.RegisterApi<AuthorizeApi>(
  AuthorizeApi,
  resolver,
  APIs.AuthorizeApi
);

export default resolver;
