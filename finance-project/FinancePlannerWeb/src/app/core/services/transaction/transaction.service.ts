import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Transaction } from '../../interfaces/transaction/transaction';
import { CreateTransaction } from '../../interfaces/transaction/create-transaction';
import { UpdateTransaction } from '../../interfaces/transaction/update-transaction';

@Injectable({
    providedIn: 'root'
})
export class TransactionService {

    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/transaction`;

    getAll(){

        return this.http.get<Transaction[]>(this.api);

    }

    create(transaction: CreateTransaction) {

        return this.http.post<void>(this.api, transaction);
    }

    update(publicId: string, transaction: UpdateTransaction) {

    return this.http.put(`${this.api}/${publicId}`,transaction);

}   

    delete(publicId: string){
        return this.http.delete<void>(`${this.api}/${publicId}`);
    }

}