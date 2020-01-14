import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, CreateButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AteTests({ items, fetchItems, loading, clearSearch, history }) {
    const forecastItems = items.map(item => ({
        ...item,
        name: item.testId,
        description: item.partNumber
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Fragment>
                        <CreateButton createUrl="/production/quality/ate-tests/create" />
                    </Fragment>
                    <Typeahead
                        items={forecastItems.map(i => ({ ...i, name: i.testId }))}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Search By Test Id"
                        history={history}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AteTests.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

AteTests.defaultProps = {
    loading: false
};

export default AteTests;
