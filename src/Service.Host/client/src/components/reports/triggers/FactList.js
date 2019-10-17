import React from 'react';
import List from '@material-ui/core/List';

function FactList({ children, id }) {
    // yes I know this is just wrapping the List for the moment
    return <List id={id}>{children}</List>;
}

export default FactList;
