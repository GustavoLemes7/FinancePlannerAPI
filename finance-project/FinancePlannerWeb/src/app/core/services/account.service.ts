import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Account } from '../interfaces/account/account';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/account`;

    getAll(){

        return this.http.get<Account[]>(this.api);

    }

    delete(){
        return this.http.delete<void>('${this.api}/${publicId}');
    }

}