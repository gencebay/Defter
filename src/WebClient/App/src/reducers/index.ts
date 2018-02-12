import { combineReducers } from "redux";
import MainReducer from "./MainReducer";
import UserReducer from "./UserReducer";

export default combineReducers({
  main: MainReducer,
  user: UserReducer
});
