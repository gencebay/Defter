import {
  LOGIN_USER,
  LOGIN_USER_FAIL,
  LOGIN_USER_SUCCESS,
  LOGOUT_USER,
  Dispatcher
} from "../actions";

export interface InitialState {
  loading: boolean;
  error: null;
  // tslint:disable-next-line:no-any;
  currentUser: any;
}

const initialState: InitialState = {
  loading: false,
  error: null,
  currentUser: null
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
