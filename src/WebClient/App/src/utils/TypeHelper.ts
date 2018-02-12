import { OperationResult, OperationResultOfT } from "../Models";

export namespace TypeHelper {
  // tslint:disable-next-line:no-any
  export function isOperationT<T extends any>(
    obj: OperationResult | OperationResultOfT<T>
  ): obj is OperationResultOfT<T> {
    return (<OperationResultOfT<T>>obj).result !== undefined;
  }
}
