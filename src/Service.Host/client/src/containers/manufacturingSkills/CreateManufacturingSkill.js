import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingSkill from '../../components/manufacturingSkills/ManufacturingSkill';
import manufacturingSkillActions from '../../actions/manufacturingSkillActions';
import manufacturingSkillSelectors from '../../selectors/manufacturingSkillSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.manufacturingSkill.item),
    loading: manufacturingSkillSelectors.getLoading(state),
    snackbarVisible: manufacturingSkillSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(manufacturingSkillActions.clearErrorsForItem());
    dispatch(manufacturingSkillActions.setEditStatus('create'));
};

const mapDispatchToProps = {
    initialise,
    addItem: manufacturingSkillActions.add,
    setEditStatus: manufacturingSkillActions.setEditStatus,
    setSnackbarVisible: manufacturingSkillActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ManufacturingSkill));
