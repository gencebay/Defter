interface ResultHandler {
  id: string;
  name: string;
  equals(obj: OperationResultHandler): boolean;
}

export default class OperationResultHandler implements ResultHandler {
  id: string;
  name: string;
  // tslint:disable-next-line:no-any
  behavior: any;
  // tslint:disable-next-line:no-any
  constructor(name: string, behavior: any) {
    this.name = name;
    this.behavior = behavior;
    this.id = `${name}-${behavior}`;
  }

  equals(obj: OperationResultHandler): boolean {
    if (obj == null) {
      return false;
    }
    return this.id === obj.id;
  }
}
