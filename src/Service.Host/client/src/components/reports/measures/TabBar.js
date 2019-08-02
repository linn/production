import React from 'react';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';

function tabProps(index) {
    return {
        id: `tab-${index}`,
        'aria-controls': `tabpanel-${index}`
    };
}

function TabBar(props) {
    const { value, onChange } = props;

    return (
        <AppBar position="static" color="default">
            <Tabs
                value={value}
                onChange={onChange}
                indicatorColor="primary"
                textColor="primary"
                variant="scrollable"
                scrollButtons="auto"
                aria-label="osr-tabs"
            >
                <Tab label="Trigger System" {...tabProps(0)} />
                <Tab label="Delivery Perf" {...tabProps(1)} />
                <Tab label="Shortages" {...tabProps(2)} />
                <Tab label="Back Orders" {...tabProps(3)} />
                <Tab label="Built This Week" {...tabProps(4)} />
                <Tab label="Stock" {...tabProps(5)} />
            </Tabs>
        </AppBar>
    );
}

TabBar.propTypes = {
    value: PropTypes.number.isRequired,
    onChange: PropTypes.func.isRequired
};

export default TabBar;
