import { Headers, Http, RequestOptions } from '@angular/http';
import 'rxjs/Rx';

export class Baseservice {
    protected url: string = "http://localhost:58614/api/";
    //url: string = "http://sql2012vm:3000/api/"
    headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
    options = new RequestOptions({ headers: this.headers });
    http: Http;

    constructor(rsc: string, http: Http) {
        this.url = this.url.concat(rsc);
        this.http = http;
    }

    getAll() {
        return this.getRequest(this.url);
    }

    getById(id: any) {
        return this.getRequest(this.url + "/" + id);
    }

    protected getRequest(url: string) {
        return this.http.get(url)
            .map(response => response.json());
    }

    add(body: any) {
        return this.http.post(this.url, body, this.options).map(response => response.json());
    }
    delete(id: number) {
        return this.http.delete(this.url + '/' + id).map(response => response.json());
    }
    update(body: any) {
        return this.http.put(this.url, body, this.options).map(response => response.json());
    }
}
