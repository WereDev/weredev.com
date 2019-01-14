export interface ICityAlbum {
    iconUrl: string;
    id: string;
    description: string;
    name: string;

}

export interface ICountry {
    cities: ICityAlbum[];
    key: string;
    name: string;
}

export interface IUrlParams {
    countryName: string;
}