export namespace ApiUtils {
  // tslint:disable-next-line:no-any
  export function emptyPromise(val: any = null): Promise<any> {
    return new Promise(resolve => {
      resolve(val);
    });
  }
}
