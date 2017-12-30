import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'leaguecalculator',
    template: 
    `<button (click)="updateItems()">Update Items</button><button (click)="updateChampions()">Update Champions</button>
    <div>
        <img *ngFor="let c of champions" [src]="c.image.url" [title]="c.name">
    </div>`,
    styleUrls: ['./leaguecalculator.component.css']
})
export class LeagueCalculatorComponent implements OnInit {

    public items: Array<Item>;
    public champions: Array<Champion>;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'api/LeagueCalculator/Items').subscribe(result => {
            this.items = result.json() as Array<Item>;
            //console.log(this.itemList);
        }, error => console.error(error));

        http.get(baseUrl + 'api/LeagueCalculator/Champions').subscribe(result => {
            this.champions = result.json() as Array<Champion>;
            console.log(this.champions[0].image);
            //console.log(this.itemList);
        }, error => console.error(error));
    }

    ngOnInit(): void { 
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/sw.js')
                .then(function(reg) {
                    // registration worked
                    console.log('Registration succeeded. Scope is ' + reg.scope);
                }).catch(function(error) {
                    // registration failed
                    console.log('Registration failed with ' + error);
                });
          }
    }

    updateItems(): void {
        this.http.get(this.baseUrl + 'api/LeagueCalculator/UpdateItems').subscribe(result => {
            console.log('done updating items');
        }, error => console.error(error));
    }

    updateChampions(): void {
        this.http.get(this.baseUrl + 'api/LeagueCalculator/UpdateChampions').subscribe(result => {
            console.log('done updating champions');
        }, error => console.error(error));
    }
}

interface Item {
    gold: any, // Gold
    plaintext: string,
    hideFromAll: boolean,
    inStore: boolean,
    into: string[],
    id: number,
    stats: any, // InventoryDataStats
    colloq: string,
    maps: [string, boolean],
    specialRecipe: number,
    image: any, // Image
    description: string,
    tags: string[],
    effect: [string, string],
    requiredChampion: string,
    from: string[],
    group: string,
    consumeOnFull: boolean,
    name: string,
    consumed: boolean,
    sanitizedDescription: string,
    depth: number,
    stacks: number
}

interface Champion {
    info: any,
    enemyTips: any,
    stats: any,
    name: any,
    title: any,
    image: any,
    tags: any,
    parType: any,
    skins: any,
    passive: any,
    recommended: any,
    allyTips: any,
    key: any,
    lore: any,
    id: any,
    blurb: any,
    spells: any
}

