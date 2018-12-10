import * as React from 'react';
import * as Collection from './collection';
import './index.scss';
import { IFlickrCollection } from './services/collectionTreeModels';
import { FlickrProvider } from './services/flickrProvider';

class Index extends React.Component<any, IndexState, any> {
  
  public componentWillMount() {
    this.setState({
      flickrCollections: [],
      flickrProvider: new FlickrProvider()
    });
  }

  public componentDidMount() {
    this.state.flickrProvider.getCollections()
    .then(response => {
      this.setState({ flickrCollections: response });
    });
  }

  public render() {
    let sortedCollections : IFlickrCollection[] = [];
    if (this.state.flickrCollections) {
      sortedCollections = this.state.flickrCollections.sort((n1, n2) => {
        if (n1.title > n2.title) { return 1; }
        if (n1.title < n2.title) { return -1; }
        return 0;
      })
    }

    const collectionTags : any[] = [];
    sortedCollections.forEach(element => {
          collectionTags.push(<Collection.default collection={element} />)
    });

    return (
      <div>
        <h1>world traveler</h1>
        <p>
          I love to travel and along the way I've become a bit of a photographer.  I'm not particularly good<br />
          though there are some pictures I'm proud of.  They're hosted at Flickr, but I'm making them available<br />
          here too.  Enjoy!
        </p>
        <p>
          Note: This site is a work in progress so not everything is clickable yet.
        </p>
        <div className="travel-collections">{ collectionTags }</div>
      </div>
    );
  }
}

export default Index;

interface IndexState {
  flickrProvider: FlickrProvider,
  flickrCollections: IFlickrCollection[]
}