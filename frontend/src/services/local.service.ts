import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class LocalService {
    public static AuthTokenName = "auth-token";

    private isLocalStorageAvailable(): boolean {
        const isWindowDefined = typeof window !== 'undefined';
        const isLocalStorageDefined = isWindowDefined && window.localStorage !== undefined;
        return isLocalStorageDefined;
    }

    isAuthenticated(name: string): boolean {
        if (this.isLocalStorageAvailable()) {
            return !!localStorage.getItem(name);
        }

        return false;
    }

    put(name: string, value: string): void {
      localStorage.setItem(name, value);
    }

    get(name: string): string | null {
      return localStorage.getItem(name);
    }

    remove(name: string): void {
      localStorage.removeItem(name);
    }
}
