import React, { Fragment } from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Collapse from '@material-ui/core/Collapse';
import ExpandLess from '@material-ui/icons/ExpandLess';
import ExpandMore from '@material-ui/icons/ExpandMore';
import PropTypes from 'prop-types';

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

    const showExpand = () => {
        if (children) {
            return !open ? <ExpandLess /> : <ExpandMore />;
        }

        return <Fragment />;
    };

    return (
        <Fragment>
            <ListItem button onClick={handleClick}>
                <ListItemAvatar>
                    <Fragment>
                        {showAvatar(avatar) ? <Avatar>{avatar}</Avatar> : <Fragment />}
                    </Fragment>
                </ListItemAvatar>
                <ListItemText primary={header} secondary={secondary} />
                {showExpand()}
            </ListItem>
            <Collapse in={!open} timeout="auto" unmountOnExit>
                {children}
            </Collapse>
        </Fragment>
    );
}

FactListItem.propTypes = {
    children: PropTypes.shape({}),
    header: PropTypes.shape({}),
    secondary: PropTypes.shape({}),
    avatar: PropTypes.shape({})
};

FactListItem.defaultProps = {
    children: null,
    header: null,
    secondary: null,
    avatar: null
};

export default FactListItem;
