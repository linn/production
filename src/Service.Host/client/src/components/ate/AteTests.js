import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, CreateButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AteTests({ items, fetchItems, loading, history }) {
    const searchItems = items.map(item => ({
        ...item,
        id: item.testId,
        name: item.testId.toString(),
        description: item.partNumber
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <>
                        <CreateButton createUrl="/production/quality/ate-tests/create" />
                    </>
                    <Typeahead
                        items={searchItems}
                        fetchItems={fetchItems}
                        clearSearch={() => {}}
                        loading={loading}
                        title="Search By Works Order"
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
    history: PropTypes.shape({}).isRequired
};

AteTests.defaultProps = {
    loading: false
};

export default AteTests;
