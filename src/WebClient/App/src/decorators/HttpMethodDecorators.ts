import { HttpMethodDefinition } from "../models";

const httpMethodMetadataKey = Symbol("design:httpMethod");

export function httpMethod(definition: HttpMethodDefinition) {
  if (!definition.body) {
    definition.body = "json";
  }
  return Reflect.metadata(httpMethodMetadataKey, definition);
}

export function getMethodDefinition(
  // tslint:disable-next-line:no-any
  target: any,
  propertyKey: string
): HttpMethodDefinition {
  return <HttpMethodDefinition>Reflect.getMetadata(
    httpMethodMetadataKey,
    target,
    propertyKey
  );
}
