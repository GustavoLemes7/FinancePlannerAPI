import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Investment } from '../../interfaces/investment/investment';
import { CreateInvestment } from '../../interfaces/investment/create-investment';
import { UpdateInvestment } from '../../interfaces/investment/update-investment';

@Injectable({
    providedIn: 'root'
})
export class InvestmentService {

    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/investment`;

    getAll(){

        return this.http.get<Investment[]>(this.api);

    }

    create(investment: CreateInvestment) {

        return this.http.post<void>(this.api, investment);
    }

    update(publicId: string, investment: UpdateInvestment) {

    return this.http.put(`${this.api}/${publicId}`,investment);

    }   

    delete(publicId: string){
        return this.http.delete<void>(`${this.api}/${publicId}`);
    }

}