export namespace StorageHelper {
  const TOKEN_KEY = "TOKEN_KEY";
  export function onSignIn(token: string): void {
    localStorage.setItem(TOKEN_KEY, token);
  }

  export function onSignOut(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  export function isSignedIn(): boolean {
    let item = localStorage.getItem(TOKEN_KEY);
    if (item !== undefined) {
      return true;
    }

    return  false;
  }
}
