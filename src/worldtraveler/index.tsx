import * as React from 'react';
import * as Flickr from './flickrProvider';


class Index extends React.Component<any, IndexState, any> {
  
  public componentWillMount() {
    this.setState({flickrProvider: new Flickr.FlickrProvider()});
  }

  public componentDidMount() {
    this.state.flickrProvider.getCollections()
    .then(response => {
      this.setState({ collections: response });
    });
  }
  

  public render() {
    const collections : any[] = [];
    if (this.state.collections) {
      this.state.collections.forEach(element => {
            collections.push(<div>{element.title}</div>)
      });
    }
    return (
      <div>
        {collections}
      </div>
    );
  }
}

export default Index;

interface IndexState {
  flickrProvider: Flickr.FlickrProvider,
  collections: Flickr.IFlickrCollection[]
}