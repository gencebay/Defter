import { OperationResult, OperationResultOfT } from "../models/OperationResult";
import { LoginViewModel } from "../models/LoginViewModel";
import { LoginResult } from "../models/LoginResult";
import { ApiContract } from "./ApiContract";
import { httpMethod } from "../decorators/HttpMethodDecorators";
import { ApiUtils } from "./ApiUtils";

export class AuthorizeApi extends ApiContract {
  constructor() {
    super("main", "api/authorize");
  }

  test = function() {
    return null;
  };

  @httpMethod({ httpMethod: "post" })
  mobileToken = (
    model: LoginViewModel
  ): Promise<OperationResultOfT<LoginResult>> | Promise<OperationResult> => ApiUtils.emptyPromise()

  // default http method is get!
  operation = (
    i: number,
    s: string,
    l: number,
    date: Date
    // tslint:disable-next-line:no-any
  ): Promise<OperationResultOfT<any>> | Promise<OperationResult> => ApiUtils.emptyPromise()

  getType = (): string => "AuthorizeApi";
}
