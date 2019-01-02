export interface ICityAlbum {
    id: string;
    name: string;
    iconUrl: string;
}

export interface ICountry {
    name: string;
    cities: ICityAlbum[];
}