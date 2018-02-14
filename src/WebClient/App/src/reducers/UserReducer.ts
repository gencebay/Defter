import {
  LOGIN_USER,
  LOGIN_USER_FAIL,
  LOGIN_USER_SUCCESS,
  LOGOUT_USER
} from "../Actions";

import { Dispatcher } from "../Constants";
import { User } from "../Models";

export interface InitialState {
  loading: boolean;
  error: null;
  currentUser: User;
}

const initialState: InitialState = {
  loading: false,
  error: null,
  currentUser: <User>{}
};

export default (state: InitialState = initialState, action: Dispatcher) => {
  switch (action.type) {
    case LOGIN_USER: {
      return { ...state, loading: true };
    }
    case LOGIN_USER_SUCCESS: {
      return { ...state, error: null, loading: false, data: action.payload };
    }
    case LOGIN_USER_FAIL: {
      return { ...state, loading: false };
    }
    case LOGOUT_USER: {
      return { ...state, loading: false };
    }
    default:
      return state;
  }
};
