import OperationResultHandler from "./OperationResultHandler";

export interface OperationResult {
  handler: OperationResultHandler;
  message: string;
  // tslint:disable-next-line:no-any
  value: any;
}

// tslint:disable-next-line:no-any
export interface OperationResultOfT<T extends any> extends OperationResult {
  result: T;
}
