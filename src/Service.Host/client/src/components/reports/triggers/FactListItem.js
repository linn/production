import React, { Fragment } from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Collapse from '@material-ui/core/Collapse';
import ExpandLess from '@material-ui/icons/ExpandLess';
import ExpandMore from '@material-ui/icons/ExpandMore';

function FactListItem({ children, header, secondary, avatar }) {
    function showAvatar() {
        if (avatar === 0) return '0';
        return avatar;
    }

    const [open, setOpen] = React.useState(true);

    const handleClick = () => {
        if (children) {
            setOpen(!open);
        }
    };

    // yes I know this is just wrapping the List for the moment
    return (
        <Fragment>
            <ListItem button onClick={handleClick}>
                <ListItemAvatar>
                    <Fragment>
                        {showAvatar(avatar) ? <Avatar>{avatar}</Avatar> : <Fragment />}
                    </Fragment>
                </ListItemAvatar>
                <ListItemText primary={header} secondary={secondary} />
                {children ? !open ? <ExpandLess /> : <ExpandMore /> : <Fragment />}
            </ListItem>
            <Collapse in={!open} timeout="auto" unmountOnExit>
                {children}
            </Collapse>
        </Fragment>
    );
}

export default FactListItem;
