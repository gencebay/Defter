import { Container } from "inversify";

import { TypeConstants } from "./Constants";
import { ApiConstants } from "./Constants";
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
} from "./Core";

import { AuthorizeApi } from "./Api";
import { InversifyHelper } from "./Utils";

let resolver = new Container();
resolver
  .bind<Settings>(TypeConstants.Settings)
  .to(DefaultSettings)
  .inSingletonScope();
resolver
  .bind<RoundRobinManager>(TypeConstants.RoundRobinManager)
  .to(RoundRobinManager)
  .inSingletonScope();
resolver
  .bind<HeaderProvider>(TypeConstants.HeaderProvider)
  .to(DefaultHeaderProvider)
  .inSingletonScope();
resolver.bind<RestClient>(TypeConstants.RestClient).to(DefaultRestClient);
resolver.bind<ProxyManager>(TypeConstants.ProxyManager).to(DefaultProxyManager);

// APIs
InversifyHelper.RegisterApi<AuthorizeApi>(
  AuthorizeApi,
  resolver,
  ApiConstants.AuthorizeApi
);

export default resolver;
