import {
  MAIN_FETCH_START,
  MAIN_FETCH_SUCCESS,
  MAIN_FETCH_FAIL,
  MAIN_ALBUM_SELECT,
  Dispatch
} from "./Types";
import { Factory } from "../factory";
import { Album } from '../models';

export const mainDataFetch = () => {
  const albumApi = Factory.createAlbumApi();

  return async (dispatch: Dispatch) => {
    try {
      dispatch({ type: MAIN_FETCH_START });
      var albums = await albumApi.list();
      dispatch({ type: MAIN_FETCH_SUCCESS, payload: albums });
    } catch (error) {
      console.log("MAIN_FETCH_FAIL:", error);
      dispatch({ type: MAIN_FETCH_FAIL, payload: error });
    }
  };
};

export const onAlbumSelect = (album:Album) => {
  return (dispatch: Dispatch) => {
    dispatch({ type: MAIN_ALBUM_SELECT, payload: album });
  };
};
