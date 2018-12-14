import * as React from 'react';
import { NavLink } from 'react-router-dom';

class Index extends React.Component {
  public render() {
    return (
      <div className="row text-center">
        <div className="col-lg-4 col-12 mt-4">
          <h2>geek</h2>
          <p>
            I play games.  I roll dice.<br />
            And sometimes create things to help.
          </p>
          <p>
            {/* <NavLink to="/geek" className="button">Would you like to know more?</NavLink> */}
            * coming soon...
          </p>
        </div>
        <div className="col-lg-4 col-12 mt-4">
          <h2>developer</h2>
          <p>
            I write software for paychecks.<br />
            And sometimes because I want stuff.
          </p>
          <p>
            {/* <NavLink to="/developer" className="button">Would you like to know more?</NavLink> */}
            * coming soon...
          </p>
        </div>
        <div className="col-lg-4 col-12 mt-4">
          <h2>world traveler</h2>
          <p>
            Work to live, don't live to work.<br />
            And make sure there's pictures.
          </p>
          <p>
            <NavLink to="/traveler" className="btn btn-secondary">Would you like to see more?</NavLink>
          </p>
        </div>
      </div>
    );
  }
}

export default Index;
