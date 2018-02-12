import { ApiContract } from "./ApiContract";
import { AlbumCategoryResult } from "../models/AlbumCategoryResult";
import { ApiUtils } from "./ApiUtils";

export class AlbumApi extends ApiContract {
  constructor() {
    super("main", "api/album");
  }

  list = (): Promise<AlbumCategoryResult[]> => ApiUtils.emptyPromise();

  getType = (): string => "AlbumApi";
}