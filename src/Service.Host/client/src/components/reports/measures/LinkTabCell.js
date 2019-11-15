import React from 'react';
import PropTypes from 'prop-types';
import makeStyles from '@material-ui/styles/makeStyles';
import TabCell from './TabCell';

const useStyles = makeStyles(theme => ({
    hover: {
        color: theme.palette.primary.main,
        padding: 0,
        cursor: 'pointer',
        textDecoration: 'underline',
        background: theme.palette.action.hover
    },
    link: {
        color: theme.palette.primary.main,
        padding: 0,
        textDecoration: 'none'
    }
}));

function LinkTabCell(props) {
    const { children, href, hoverHref, setHoverHref, ...other } = props;
    const classes = useStyles();

    function onMouseEnter() {
        setHoverHref(href);
    }

    function onMouseLeave() {
        setHoverHref(null);
    }

    return (
        <TabCell
            className={href === hoverHref ? classes.hover : classes.link}
            href={href}
            onMouseEnter={onMouseEnter}
            onMouseLeave={onMouseLeave}
            {...other}
        >
            <a href={href}> {children} </a>
        </TabCell>
    );
}

LinkTabCell.propTypes = {
    children: PropTypes.node,
    href: PropTypes.string,
    hoverHref: PropTypes.string,
    setHoverHref: PropTypes.func
};

export default LinkTabCell;
