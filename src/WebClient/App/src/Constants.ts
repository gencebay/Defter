export const TypeConstants = {
  Settings: Symbol("Settings"),
  RestClient: Symbol("RestClient"),
  HeaderProvider: Symbol("HeaderProvider"),
  ProxyInvoker: Symbol("ProxyInvoker"),
  ProxyManager: Symbol("ProxyManager"),
  RoundRobinManager: Symbol("RoundRobinManager")
};

export const ApiConstants = {
  AuthorizeApi: "Factory<AuthorizeApi>",
  AlbumApi: "Factory<AlbumApi>"
};

export const ActionConstants = {
  UserActions: Symbol("UserActions")
};

export interface Dispatcher {
  type: string;
  // tslint:disable-next-line:no-any
  payload?: any;
}

// tslint:disable-next-line:no-any
export interface DispatcherOfT<T extends any> {
  type: string;
  // tslint:disable-next-line:no-any
  payload?: T | any;
}

export interface Dispatch {
  (dispatcher: Dispatcher): void;
}

export interface DispatchOfT<T> {
  (dispatcher: DispatcherOfT<T>): T;
}
