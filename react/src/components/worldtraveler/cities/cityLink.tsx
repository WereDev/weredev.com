import * as React from 'react';
import { NavLink, RouteComponentProps } from 'react-router-dom';
import { ICityAlbum, IUrlParams } from 'src/models/worldTraveler';

class CityLink extends React.Component<ICityLinkProps, any, any> {

    private cityName : string;

    public componentWillMount() {
        this.cityName = this.props.city.name;
        const commaIndex = this.cityName.indexOf(',');
        if (commaIndex > 0) {
            this.cityName = this.cityName.substring(0, commaIndex);
        }
    }

    public render() {
        let cityUrl = this.props.match.url + '/' + this.cityName;
        cityUrl = cityUrl.replace('//','/');
        return (
            <tr>
                <td className="city-list-cell-left">
                    <NavLink to={cityUrl}>
                        <img src={this.props.city.iconUrl} className='border border-dark' />
                    </NavLink>
                </td>
                <td className="city-list-cell-right">
                    <NavLink to={cityUrl}>
                        <h4 className='mt-1'>{this.props.city.name}</h4>
                    </NavLink>
                    {this.props.city.description}
                </td>
            </tr>
        );
    }
}

export default CityLink;

export interface ICityLinkProps extends RouteComponentProps<IUrlParams> {
    city: ICityAlbum;
}
