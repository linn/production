import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingSkill from '../../components/manufacturingSkills/ManufacturingSkill';
import manufacturingSkillActions from '../../actions/manufacturingSkillActions';
import manufacturingSkillSelectors from '../../selectors/manufacturingSkillSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: manufacturingSkillSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: manufacturingSkillSelectors.getEditStatus(state),
    loading: manufacturingSkillSelectors.getLoading(state),
    snackbarVisible: manufacturingSkillSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.manufacturingSkill.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(manufacturingSkillActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: manufacturingSkillActions.update,
    setEditStatus: manufacturingSkillActions.setEditStatus,
    setSnackbarVisible: manufacturingSkillActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ManufacturingSkill));
