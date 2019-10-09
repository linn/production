import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import NotesIcon from '@material-ui/icons/Notes';
import Page from '../../../containers/Page';
import FactList from './FactList';
import FactListItem from './FactListItem';
import WorksOrderList from './WorksOrderList';

function ProductionTriggerFacts({ reportData, loading, options, history, itemError }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="The Facts about a production part" />
                    {loading ? <Loading /> : ''}
                </Grid>
                {itemError ? (
                    <ErrorCard errorMessage={itemError.details?.message}/>
                ) : (
                    <Grid item xs={12}>
                        {reportData ? (
                            <Fragment>
                                <FactList>
                                    <FactListItem
                                        header={reportData.partNumber}
                                        secondary={reportData.description}
                                    />
                                    <FactListItem
                                        header="Build"
                                        secondary="Build this to satisfy internal customers and trigger level"
                                        avatar={reportData.reqtForInternalAndTriggerLevelBT}
                                    >
                                        <span>Test Expanded</span>
                                    </FactListItem>
                                    <FactListItem
                                        header="Qty Free"
                                        secondary="Good stock available to use"
                                        avatar={reportData.qtyFree}
                                    />
                                    <FactListItem
                                        header="Qty Being Built"
                                        secondary="Outstanding works order qty"
                                        avatar={reportData.qtyBeingBuilt}
                                    >
                                        <WorksOrderList
                                            worksOrders={reportData.outstandingWorksOrders}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Back-Order Requirement"
                                        secondary="Required for customer back orders"
                                        avatar={reportData.reqtForSalesOrdersBE}
                                    />
                                    <FactListItem
                                        header="Kanban Size"
                                        secondary="You must always build in multiples of this"
                                        avatar={reportData.kanbanSize}
                                    />
                                    <FactListItem
                                        header="Trigger Level"
                                        secondary="You should have at least this in stock after customers have been satisfied"
                                        avatar={reportData.triggerLevel}
                                    >
                                        <span>Test Expanded</span>
                                    </FactListItem>
                                    <FactListItem
                                        header="Priority"
                                        secondary="This part is needed to satisfy back orders"
                                        avatar={reportData.priority}
                                    >
                                        <span>Test Expanded</span>
                                    </FactListItem>
                                    <FactListItem
                                        header="Story"
                                        secondary={
                                            reportData.story ? reportData.story : 'No story entered'
                                        }
                                        avatar={<NotesIcon />}
                                    />
                                </FactList>
                            </Fragment>
                        ) : (
                            ''
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

ProductionTriggerFacts.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    config: PropTypes.shape({})
};

ProductionTriggerFacts.defaultProps = {
    reportData: null,
    config: null,
    loading: false
};

export default ProductionTriggerFacts;
