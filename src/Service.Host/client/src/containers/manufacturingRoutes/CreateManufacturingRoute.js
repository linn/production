import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/manufacturingRoutes/ManufacturingRoute';
import manufacturingRouteActions from '../../actions/manufacturingRouteActions';
import manufacturingRouteSelectors from '../../selectors/manufacturingRouteSelectors';
import manufacturingSkillsActions from '../../actions/manufacturingSkillsActions';
import manufacturingSkillsSelectors from '../../selectors/manufacturingSkillsSelectors';
import manufacturingResourcesActions from '../../actions/manufacturingResourcesActions';
import manufacturingResourcesSelectors from '../../selectors/manufacturingResourcesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: getItemErrorDetailMessage(state),
    loading: manufacturingRouteSelectors.getLoading(state),
    snackbarVisible: manufacturingRouteSelectors.getSnackbarVisible(state),
    manufacturingSkills: manufacturingSkillsSelectors.getItems(state),
    manufacturingResources: manufacturingResourcesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
});

const initialise = () => dispatch => {
    dispatch(manufacturingRouteActions.setEditStatus('create'));
    dispatch(manufacturingRouteActions.create());
    dispatch(manufacturingSkillsActions.fetch());
    dispatch(manufacturingResourcesActions.fetch());
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: manufacturingRouteActions.add,
    setEditStatus: manufacturingRouteActions.setEditStatus,
    setSnackbarVisible: manufacturingRouteActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingRoute));
