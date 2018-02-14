import { injectable, inject } from "inversify";
import { HeaderProvider } from "./DefaultHeaderProvider";
import { TypeConstants } from "../Constants";

console.log("TYPES:", TypeConstants);

export interface RestClient {
  headerProvider: HeaderProvider;
}

@injectable()
export class DefaultRestClient {
  public headerProvider: HeaderProvider;
  public constructor(
    @inject(TypeConstants.HeaderProvider) headerProvider: HeaderProvider
  ) {
    this.headerProvider = headerProvider;
  }
}
