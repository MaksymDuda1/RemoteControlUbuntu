import { LocalService } from "../services/local.service";


export function jwtFactory(localService: LocalService){
    return{
        tokenGetter: () => {
            return localService.get(LocalService.AuthTokenName);
        }
    }
}