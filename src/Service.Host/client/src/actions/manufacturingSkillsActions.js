import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingSkillsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.manufacturingSkills.item,
    itemTypes.manufacturingSkills.actionType,
    itemTypes.manufacturingSkills.uri,
    actionTypes,
    config.appRoot
);
