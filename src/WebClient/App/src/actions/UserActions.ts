import { LoginResult, LoginViewModel } from "../models";
import {
  LOGIN_USER,
  LOGIN_USER_SUCCESS,
  LOGIN_USER_FAIL,
  LOGOUT_USER,
  Dispatch,
  DispatchOfT
} from "../actions";
import { Factory } from "../factory";
import { TypeHelper, StorageHelper } from "../utils";

export function loginUser(username: string, password: string) {
  const model: LoginViewModel = {
    username: username,
    password: password
  };

  const authorizeApi = Factory.createAuthorizeApi();

  return async (dispatch: DispatchOfT<LoginResult>) => {
    try {
      dispatch({ type: LOGIN_USER });
      var apiCall = await authorizeApi.mobileToken(model);
      if (apiCall === undefined) {
        dispatch({ type: LOGIN_USER_SUCCESS, payload: null });
      }

      if (TypeHelper.isOperationT<LoginResult>(apiCall)) {
        const token = apiCall.result.token;
        if (token) {
          await StorageHelper.onSignIn(token);
        }
        dispatch({ type: LOGIN_USER_SUCCESS, payload: apiCall.result });
      }
    } catch (error) {
      console.log({ error: error });
      dispatch({ type: LOGIN_USER_FAIL, payload: error });
    }
  };
}

export function userSignOut() {
  return async (dispatch: Dispatch) => {
    try {
      await StorageHelper.onSignOut();

      dispatch({ type: LOGOUT_USER, payload: null });
    } catch (error) {
      console.log({ error: error });
    }
  };
}
