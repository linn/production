import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/productionTriggerLevels/TriggerLevel';
import productionTriggerLevelActions from '../../actions/productionTriggerLevelActions';
import productionTriggerLevelSelectors from '../../selectors/productionTriggerLevelSelectors';
import manufacturingSkillsActions from '../../actions/manufacturingSkillsActions';
import manufacturingSkillsSelectors from '../../selectors/manufacturingSkillsSelectors';
import manufacturingResourcesActions from '../../actions/manufacturingResourcesActions';
import manufacturingResourcesSelectors from '../../selectors/manufacturingResourcesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: productionTriggerLevelSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: productionTriggerLevelSelectors.getEditStatus(state),
    loading: productionTriggerLevelSelectors.getLoading(state),
    snackbarVisible: productionTriggerLevelSelectors.getSnackbarVisible(state),
    manufacturingSkills: manufacturingSkillsSelectors.getItems(state),
    manufacturingResources: manufacturingResourcesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    itemErrors: getItemError(state, itemTypes.productionTriggerLevel.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(productionTriggerLevelActions.fetch(itemId));
    dispatch(manufacturingSkillsActions.fetch());
    dispatch(manufacturingResourcesActions.fetch());
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: productionTriggerLevelActions.update,
    setEditStatus: productionTriggerLevelActions.setEditStatus,
    setSnackbarVisible: productionTriggerLevelActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ManufacturingRoute));
