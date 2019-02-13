export interface IListCountriesResponse {
    countries: ICountry[]
}

export interface ICountry {
    key: string;
    name: string;
}