import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'itemcalculator',
    template: `<button (click)="updateItems()">Update Items</button><button (click)="updateChampions()">Update Champions</button>`
})
export class ItemCalculatorComponent {

    public items: ItemList;
    public champions: ChampionList;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'api/LeagueCalculator/Items').subscribe(result => {
            this.items = result.json() as ItemList;
            //console.log(this.itemList);
        }, error => console.error(error));

        http.get(baseUrl + 'api/LeagueCalculator/Champions').subscribe(result => {
            this.champions = result.json() as ChampionList;
            //console.log(this.itemList);
        }, error => console.error(error));
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

interface ItemList {
    data: any, // update this
    version: string,
    tree: any, // update this
    groups: any, // update this
    type: string
}

interface ItemTree {
    header: string,
    tags: any // update this
}

interface Item {
    gold: any, // update this
    plaintext: string,
    hideFromAll: boolean,
    inStore: boolean,
    into: any, // update this
    id: number, // update this
    stats: any, // update this
    colloq: string // update this
    maps: any, // update this
    specialRecipe: any, // update this
    image: any, // update this
    description: string,
    tags: any, // update this
    effect: any, // update this
    requiredChampion: string,
    from: any, // update this
    group: string, // update this
    consumeOnFull: boolean,
    name: string,
    consumed: boolean,
    sanitizedDescription: string,
    depth: number,
    stack: number
}

interface ChampionList {
    data: any, // update this
    version: string,
    keys: any, // update this
    format: any, // update this
    type: string
}