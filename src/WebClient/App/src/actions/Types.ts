export const LOGIN_USER_SUCCESS = "login_user_success";
export const LOGIN_USER_FAIL = "login_user_fail";
export const LOGIN_USER = "login_user";
export const LOGOUT_USER = "logout_user";

export const REGISTER_USER = "register_user";
export const REGISTER_USER_SUCCESS = "register_user_success";
export const REGISTER_USER_FAIL = "register_user_fail";

export const SET_CURRENT_USER = "set_current_user";
export const FETCH_USER = "fetch_user";
export const FETCH_USERS = "fetch_users";
export const SET_USER_AVATAR = "set_user_avatar";

export const REPORTS_FETCH_START = "reports_fetch_start";
export const REPORTS_FETCH_SUCCESS = "reports_fetch_success";
export const REPORTS_FETCH_FAIL = "reports_fetch_fail";

export const MAIN_FETCH_START = "main_fetch_start";
export const MAIN_FETCH_SUCCESS = "main_fetch_success";
export const MAIN_FETCH_FAIL = "main_fetch_fail";
export const MAIN_ALBUM_SELECT = "main_album_select";

export const OFFER_FETCH_START = "offer_fetch_start";
export const OFFER_FETCH_SUCCESS = "offer_fetch_success";
export const OFFER_FETCH_FAIL = "offer_fetch_fail";

const TYPES = {
  Settings: Symbol("Settings"),
  RestClient: Symbol("RestClient"),
  HeaderProvider: Symbol("HeaderProvider"),
  ProxyInvoker: Symbol("ProxyInvoker"),
  ProxyManager: Symbol("ProxyManager"),
  RoundRobinManager: Symbol("RoundRobinManager")
};

const APIs = {
  AuthorizeApi: "Factory<AuthorizeApi>",
  AlbumApi: "Factory<AlbumApi>"
};

const ACTIONs = {
  UserActions: Symbol("UserActions")
};

interface Dispatcher {
  type: string;
  // tslint:disable-next-line:no-any
  payload?: any;
}

// tslint:disable-next-line:no-any
interface DispatcherOfT<T extends any> {
  type: string;
  // tslint:disable-next-line:no-any
  payload?: T | any
}

interface Dispatch {
  (dispatcher: Dispatcher): void;
}

interface DispatchOfT<T> {
  (dispatcher: DispatcherOfT<T>): T;
}

export { TYPES, APIs, ACTIONs, Dispatcher, Dispatch, DispatcherOfT, DispatchOfT };