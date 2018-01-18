import { Component } from '@angular/core';
import { SystemSettingsService } from './services/system-settings.service';

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html',
    styleUrls: [ 'app.component.css'],
})
export class AppComponent { 
    name = 'Angular 2';

    constructor(private sysSettings : SystemSettingsService){

        this.sysSettings.getAll().subscribe(result => {
            localStorage.setItem("pageSize", result.find(x => x.Key === 'UI.System.PageSize').Value);
            localStorage.setItem("DocTypes", result.find(x => x.Key === 'UI.Document.Download.Types').Value);
            localStorage.setItem("DownloadUrl", result.find(x => x.Key === 'API.Document.Download.Url').Value);
        });


    }

}
