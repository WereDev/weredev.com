import * as React from 'react';
import * as Collections from './collections';
import * as Flickr from './flickrProvider';



class Index extends React.Component<any, IndexState, any> {
  
  public componentWillMount() {
    this.setState({
      flickrCollections: [],
      flickrProvider: new Flickr.FlickrProvider()
    });
  }

  public componentDidMount() {
    this.state.flickrProvider.getCollections()
    .then(response => {
      this.setState({ flickrCollections: response });
    });
  }
  

  public render() {
    return (
      <Collections.default collections={ this.state.flickrCollections } />
    );
  }
}

export default Index;


interface IndexState {
  flickrProvider: Flickr.FlickrProvider,
  flickrCollections: Flickr.IFlickrCollection[]
}