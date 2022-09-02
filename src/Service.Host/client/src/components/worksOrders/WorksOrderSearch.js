import React, { useState, Fragment } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import {
    Loading,
    SearchInputField,
    useSearch,
    DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function WorksOrderSearch({ loading, fetchItems, searchItems }) {
    const [searchOrderNumber, setSearchOrderNumber] = useState('');
    const [searchFromDate, setSearchFromDate] = useState(null);
    const [searchToDate, setSearchToDate] = useState(null);
    const [searchPartNumber, setSearchPartNumber] = useState();

    useSearch(
        fetchItems,
        'searchTerm',
        null,
        `${searchOrderNumber ?? ''}&fromDate=${
            searchFromDate ? searchFromDate.toISOString() : ''
        }&toDate=${searchToDate ? searchToDate.toISOString() : ''}&partNumber=${searchPartNumber ??
            ''}`
    );

    return (
        <Page>
            <Grid container spacing={3}>
                <>
                    <Grid item xs={3}>
                        <SearchInputField
                            label="Search for Order Number"
                            fullWidth
                            placeHolder="Order Number"
                            onChange={(propertyName, newValue) => setSearchOrderNumber(newValue)}
                            propertyName="searchOrderNumber"
                            type="number"
                            value={searchOrderNumber}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <SearchInputField
                            label="Search for Part Number"
                            fullWidth
                            placeHolder="Part Number"
                            onChange={(propertyName, newValue) => setSearchPartNumber(newValue)}
                            propertyName="searchPartNumber"
                            value={searchPartNumber}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <DatePicker
                            value={searchFromDate}
                            label="From Date"
                            onChange={value => setSearchFromDate(value)}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <DatePicker
                            value={searchToDate}
                            label="From Date"
                            onChange={value => setSearchToDate(value)}
                        />
                    </Grid>
                </>
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        <List dense>
                            {searchItems.map(wo => (
                                <Fragment key={wo.orderNumber}>
                                    <Link
                                        component={RouterLink}
                                        to={`/production/works-orders/${wo.orderNumber}`}
                                    >
                                        <ListItem spacing={15}>
                                            <Grid item xs={3}>
                                                <Typography variant="subtitle1">
                                                    {wo.orderNumber}
                                                </Typography>
                                            </Grid>
                                            <Grid item xs={3} sx={{ mr: 2 }}>
                                                <Typography>
                                                    {moment(wo.dateRaised).format('DD MMM YYYY')}
                                                </Typography>
                                            </Grid>
                                            <Grid item xs={6}>
                                                <Typography>{wo.partNumber}</Typography>
                                            </Grid>
                                        </ListItem>
                                    </Link>

                                    <Divider component="li" />
                                </Fragment>
                            ))}
                        </List>
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

WorksOrderSearch.propTypes = {
    fetchItems: PropTypes.arrayOf(PropTypes.shape({})),
    searchItems: PropTypes.func,
    loading: PropTypes.bool
};

WorksOrderSearch.defaultProps = {
    fetchItems: null,
    searchItems: {},
    loading: false
};

export default WorksOrderSearch;
