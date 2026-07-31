import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Account } from '../interfaces/account/account';
import { CreateAccount } from '../interfaces/account/create-account';
import { UpdateAccount } from '../interfaces/account/update-account';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/account`;

    getAll(){

        return this.http.get<Account[]>(this.api);

    }

    create(account: CreateAccount) {

        return this.http.post<void>(this.api, account);
    }

    update(publicId: string, account: UpdateAccount) {

    return this.http.put(`${this.api}/${publicId}`,account);

}   

    delete(publicId: string){
        return this.http.delete<void>(`${this.api}/${publicId}`);
    }

}