import { injectable } from "inversify";
import { GuidHelper } from "../utils";

export interface HeaderProvider {
  guid: string;
  headers: Headers;
}

@injectable()
export class DefaultHeaderProvider implements HeaderProvider {
  public guid: string;
  public headers: Headers;
  constructor() {
    this.guid = GuidHelper.uuidv4();
    this.headers = new Headers();
    this.headers.append("X-MyApp-Client", "1234567890abcdef");
  }
}
