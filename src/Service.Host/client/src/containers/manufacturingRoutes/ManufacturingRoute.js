﻿import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/manufacturingRoutes/ManufacturingRoute';
import manufacturingRouteActions from '../../actions/manufacturingRouteActions';
import manufacturingRouteSelectors from '../../selectors/manufacturingRouteSelectors';
import manufacturingSkillsActions from '../../actions/manufacturingSkillsActions';
import manufacturingSkillsSelectors from '../../selectors/manufacturingSkillsSelectors';
import manufacturingResourcesActions from '../../actions/manufacturingResourcesActions';
import manufacturingResourcesSelectors from '../../selectors/manufacturingResourcesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';

const mapStateToProps = (state, { match }) => ({
    item: manufacturingRouteSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: manufacturingRouteSelectors.getEditStatus(state),
    loading: manufacturingRouteSelectors.getLoading(state),
    snackbarVisible: manufacturingRouteSelectors.getSnackbarVisible(state),
    manufacturingSkills: manufacturingSkillsSelectors.getItems(state),
    manufacturingResources: manufacturingResourcesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(manufacturingRouteActions.fetch(itemId));
    dispatch(manufacturingSkillsActions.fetch());
    dispatch(manufacturingResourcesActions.fetch());
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: manufacturingRouteActions.update,
    setEditStatus: manufacturingRouteActions.setEditStatus,
    setSnackbarVisible: manufacturingRouteActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingRoute));
