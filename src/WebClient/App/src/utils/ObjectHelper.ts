const STRIP_COMMENTS = /((\/\/.*$)|(\/\*[\s\S]*?\*\/))/gm;
const ARGUMENT_NAMES = /([^\s,]+)/g;

export namespace ObjectHelper {
  // tslint:disable-next-line:no-any
  export function toQueryString(obj: any) {
    return Object.keys(obj)
      .map(k => `${encodeURIComponent(k)}=${encodeURIComponent(obj[k])}`)
      .join("&");
  }

  // tslint:disable-next-line:no-any
  export function getParamNames(func: any) {
    var fnStr = func.toString().replace(STRIP_COMMENTS, "");
    var result = fnStr
      .slice(fnStr.indexOf("(") + 1, fnStr.indexOf(")"))
      .match(ARGUMENT_NAMES);
    if (result === null) {
      result = [];
    }
    return result;
  }
}
