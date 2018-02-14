import { injectable } from "inversify";
import { GuidHelper } from "../Utils";

interface HeaderProvider {
  guid: string;
  headers: Headers;
}

@injectable()
class DefaultHeaderProvider implements HeaderProvider {
  public guid: string;
  public headers: Headers;
  constructor() {
    this.guid = GuidHelper.uuidv4();
    this.headers = new Headers();
    this.headers.append("X-MyApp-Client", "1234567890abcdef");
  }
}

export { HeaderProvider, DefaultHeaderProvider };
